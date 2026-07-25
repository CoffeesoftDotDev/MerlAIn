import { registry } from '../moduleRegistry';

export default function DashboardPage() {
  return (
    <>
      <h1 className="page__title">Dashboard</h1>
      <p className="page__subtitle">
        Every card below is contributed by a module manifest. The shell renders them without
        knowing what any module does.
      </p>

      <section className="card-grid">
        {registry.widgets.map((widget) => {
          const Widget = widget.component;
          return (
            <article className="card" key={widget.id}>
              <h2 className="card__title">{widget.title}</h2>
              <Widget />
              <p className="card__body">
                <span className="badge">{widget.moduleId}</span>
              </p>
            </article>
          );
        })}
      </section>

      {registry.widgets.length === 0 && (
        <p className="placeholder">No module contributes a dashboard widget yet.</p>
      )}
    </>
  );
}
