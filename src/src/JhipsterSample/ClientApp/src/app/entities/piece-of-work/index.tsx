import React from "react";
import { Switch } from "react-router-dom";

import ErrorBoundaryRoute from "app/shared/error/error-boundary-route";

import PieceOfWork from "./piece-of-work";
import PieceOfWorkDetail from "./piece-of-work-detail";
import PieceOfWorkUpdate from "./piece-of-work-update";
import PieceOfWorkDeleteDialog from "./piece-of-work-delete-dialog";

const Routes = ({ match }) => (
  <>
    <Switch>
      <ErrorBoundaryRoute
        exact
        path={`${match.url}/new`}
        component={PieceOfWorkUpdate}
      />
      <ErrorBoundaryRoute
        exact
        path={`${match.url}/:id/edit`}
        component={PieceOfWorkUpdate}
      />
      <ErrorBoundaryRoute
        exact
        path={`${match.url}/:id`}
        component={PieceOfWorkDetail}
      />
      <ErrorBoundaryRoute path={match.url} component={PieceOfWork} />
    </Switch>
    <ErrorBoundaryRoute
      exact
      path={`${match.url}/:id/delete`}
      component={PieceOfWorkDeleteDialog}
    />
  </>
);

export default Routes;
