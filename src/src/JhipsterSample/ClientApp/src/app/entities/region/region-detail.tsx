import React, { useEffect } from "react";
import { Link, RouteComponentProps } from "react-router-dom";
import { Button, Row, Col } from "reactstrap";
import {} from "react-jhipster";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { APP_DATE_FORMAT, APP_LOCAL_DATE_FORMAT } from "app/config/constants";
import { useAppDispatch, useAppSelector } from "app/config/store";

import { getEntity } from "./region.reducer";

export const RegionDetail = (props: RouteComponentProps<{ id: string }>) => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getEntity(props.match.params.id));
  }, []);

  const regionEntity = useAppSelector((state) => state.region.entity);
  return (
    <Row>
      <Col md="8">
        <h2 data-cy="regionDetailsHeading">Region</h2>
        <dl className="jh-entity-details">
          <dt>
            <span id="id">ID</span>
          </dt>
          <dd>{regionEntity.id}</dd>
          <dt>
            <span id="regionName">Region Name</span>
          </dt>
          <dd>{regionEntity.regionName}</dd>
        </dl>
        <Button
          tag={Link}
          to="/region"
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
          to={`/region/${regionEntity.id}/edit`}
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

export default RegionDetail;
