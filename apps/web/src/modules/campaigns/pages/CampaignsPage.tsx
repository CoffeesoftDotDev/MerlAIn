import { Link } from 'react-router-dom';

export default function CampaignsPage() {
  return (
    <>
      <h1 className="page__title">📜 Campaigns</h1>
      <p className="page__subtitle">
        Placeholder page served by the <code>campaigns</code> module.
      </p>

      <div className="placeholder">
        <p>
          This route was mounted from the module manifest at <code>/campaigns</code>. Campaign
          listing and creation arrive in phase 4.
        </p>
        <Link className="badge" to="/campaigns/scenarios">
          Go to scenarios →
        </Link>
      </div>
    </>
  );
}
