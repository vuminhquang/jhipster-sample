import React, { useEffect } from "react";
import { Link, RouteComponentProps } from "react-router-dom";
import { Button, Row, Col } from "reactstrap";
import {} from "react-jhipster";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { APP_DATE_FORMAT, APP_LOCAL_DATE_FORMAT } from "app/config/constants";
import { useAppDispatch, useAppSelector } from "app/config/store";

import { getEntity } from "./job.reducer";

export const JobDetail = (props: RouteComponentProps<{ id: string }>) => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getEntity(props.match.params.id));
  }, []);

  const jobEntity = useAppSelector((state) => state.job.entity);
  return (
    <Row>
      <Col md="8">
        <h2 data-cy="jobDetailsHeading">Job</h2>
        <dl className="jh-entity-details">
          <dt>
            <span id="id">ID</span>
          </dt>
          <dd>{jobEntity.id}</dd>
          <dt>
            <span id="jobTitle">Job Title</span>
          </dt>
          <dd>{jobEntity.jobTitle}</dd>
          <dt>
            <span id="minSalary">Min Salary</span>
          </dt>
          <dd>{jobEntity.minSalary}</dd>
          <dt>
            <span id="maxSalary">Max Salary</span>
          </dt>
          <dd>{jobEntity.maxSalary}</dd>
          <dt>Chore</dt>
          <dd>
            {jobEntity.chores
              ? jobEntity.chores.map((val, i) => (
                  <span key={val.id}>
                    <a>{val.title}</a>
                    {jobEntity.chores && i === jobEntity.chores.length - 1
                      ? ""
                      : ", "}
                  </span>
                ))
              : null}
          </dd>
          <dt>Employee</dt>
          <dd>{jobEntity.employee ? jobEntity.employee.id : ""}</dd>
        </dl>
        <Button
          tag={Link}
          to="/job"
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
          to={`/job/${jobEntity.id}/edit`}
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

export default JobDetail;
