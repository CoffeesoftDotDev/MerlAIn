import { Link } from 'react-router-dom';

export default function NotFoundPage() {
  return (
    <>
      <h1 className="page__title">Lost in the mists</h1>
      <p className="page__subtitle">No module claims this route.</p>
      <Link className="badge" to="/">
        Back to the dashboard
      </Link>
    </>
  );
}
