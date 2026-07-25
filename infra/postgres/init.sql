-- =============================================================================
-- MerlAIn - database bootstrap
--
-- Boundary rule: every feature module owns its own schema.
-- No cross-schema foreign keys, no cross-schema joins.
-- Tables themselves are created by each module's EF Core migrations (phase 2+).
-- =============================================================================

CREATE SCHEMA IF NOT EXISTS identity;
CREATE SCHEMA IF NOT EXISTS campaigns;
CREATE SCHEMA IF NOT EXISTS scenarios;
CREATE SCHEMA IF NOT EXISTS characters;
CREATE SCHEMA IF NOT EXISTS assets;
CREATE SCHEMA IF NOT EXISTS equipment;
CREATE SCHEMA IF NOT EXISTS maps;
CREATE SCHEMA IF NOT EXISTS agents;
