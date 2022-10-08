import React, { useEffect } from "react";
import { Link, RouteComponentProps } from "react-router-dom";
import { Button, Row, Col } from "reactstrap";
import {} from "react-jhipster";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { APP_DATE_FORMAT, APP_LOCAL_DATE_FORMAT } from "app/config/constants";
import { useAppDispatch, useAppSelector } from "app/config/store";

import { getEntity } from "./piece-of-work.reducer";

export const PieceOfWorkDetail = (
  props: RouteComponentProps<{ id: string }>
) => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getEntity(props.match.params.id));
  }, []);

  const pieceOfWorkEntity = useAppSelector((state) => state.pieceOfWork.entity);
  return (
    <Row>
      <Col md="8">
        <h2 data-cy="pieceOfWorkDetailsHeading">PieceOfWork</h2>
        <dl className="jh-entity-details">
          <dt>
            <span id="id">ID</span>
          </dt>
          <dd>{pieceOfWorkEntity.id}</dd>
          <dt>
            <span id="title">Title</span>
          </dt>
          <dd>{pieceOfWorkEntity.title}</dd>
          <dt>
            <span id="description">Description</span>
          </dt>
          <dd>{pieceOfWorkEntity.description}</dd>
        </dl>
        <Button
          tag={Link}
          to="/piece-of-work"
          replace
          color="info"
          data-cy="entityDetailsBackButton"
        >
          <FontAwesomeIcon icon="arrow-left" />{" "}
          <span className="d-none d-md-inline">Back</span>
        </Button>
        &nbsp;
        <Button
          tag={Link}
          to={`/piece-of-work/${pieceOfWorkEntity.id}/edit`}
          replace
          color="primary"
        >
          <FontAwesomeIcon icon="pencil-alt" />{" "}
          <span className="d-none d-md-inline">Edit</span>
        </Button>
      </Col>
    </Row>
  );
};

export default PieceOfWorkDetail;
