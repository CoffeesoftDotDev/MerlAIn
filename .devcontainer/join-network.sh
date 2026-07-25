#!/usr/bin/env bash
# Attaches this dev container to the MerlAIn compose network.
#
# The container talks to the host's Docker daemon (docker-outside-of-docker), so it
# can attach itself to the same user-defined bridge the stack runs on. Once attached,
# service names resolve directly: postgres:5432, redis:6379, api:8080, ...
#
# Idempotent: safe to run on every container start.
set -euo pipefail

NETWORK="${MERLAIN_NETWORK:-merlain}"

if ! docker info >/dev/null 2>&1; then
  echo "join-network: Docker socket not reachable, skipping." >&2
  exit 0
fi

# Compose creates this network on `up`, but the dev container is often started first.
if ! docker network inspect "$NETWORK" >/dev/null 2>&1; then
  echo "join-network: creating network '$NETWORK'."
  docker network create --driver bridge "$NETWORK" >/dev/null
fi

# Inside the container, /etc/hostname holds our own short container id.
SELF="$(cat /etc/hostname)"

if docker network inspect "$NETWORK" --format '{{range $id, $c := .Containers}}{{$id}}{{"\n"}}{{end}}' \
  | grep -q "^${SELF}"; then
  echo "join-network: already attached to '$NETWORK'."
  exit 0
fi

docker network connect --alias devcontainer "$NETWORK" "$SELF"
echo "join-network: attached to '$NETWORK' as 'devcontainer'."
