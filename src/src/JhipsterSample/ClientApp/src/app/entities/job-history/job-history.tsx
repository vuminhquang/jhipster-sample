import React, { useState, useEffect } from "react";
import { Link, RouteComponentProps } from "react-router-dom";
import { Button, Table } from "reactstrap";
import { Translate, TextFormat } from "react-jhipster";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { APP_DATE_FORMAT, APP_LOCAL_DATE_FORMAT } from "app/config/constants";
import { useAppDispatch, useAppSelector } from "app/config/store";

import { IJobHistory } from "app/shared/model/job-history.model";
import { getEntities } from "./job-history.reducer";

export const JobHistory = (props: RouteComponentProps<{ url: string }>) => {
  const dispatch = useAppDispatch();

  const jobHistoryList = useAppSelector((state) => state.jobHistory.entities);
  const loading = useAppSelector((state) => state.jobHistory.loading);

  useEffect(() => {
    dispatch(getEntities({}));
  }, []);

  const handleSyncList = () => {
    dispatch(getEntities({}));
  };

  const { match } = props;

  return (
    <div>
      <h2 id="job-history-heading" data-cy="JobHistoryHeading">
        Job Histories
        <div className="d-flex justify-content-end">
          <Button
            className="me-2"
            color="info"
            onClick={handleSyncList}
            disabled={loading}
          >
            <FontAwesomeIcon icon="sync" spin={loading} /> Refresh List
          </Button>
          <Link
            to="/job-history/new"
            className="btn btn-primary jh-create-entity"
            id="jh-create-entity"
            data-cy="entityCreateButton"
          >
            <FontAwesomeIcon icon="plus" />
            &nbsp; Create new Job History
          </Link>
        </div>
      </h2>
      <div className="table-responsive">
        {jobHistoryList && jobHistoryList.length > 0 ? (
          <Table responsive>
            <thead>
              <tr>
                <th>ID</th>
                <th>Start Date</th>
                <th>End Date</th>
                <th>Language</th>
                <th>Job</th>
                <th>Department</th>
                <th>Employee</th>
                <th />
              </tr>
            </thead>
            <tbody>
              {jobHistoryList.map((jobHistory, i) => (
                <tr key={`entity-${i}`} data-cy="entityTable">
                  <td>
                    <Button
                      tag={Link}
                      to={`/job-history/${jobHistory.id}`}
                      color="link"
                      size="sm"
                    >
                      {jobHistory.id}
                    </Button>
                  </td>
                  <td>
                    {jobHistory.startDate ? (
                      <TextFormat
                        type="date"
                        value={jobHistory.startDate}
                        format={APP_DATE_FORMAT}
                      />
                    ) : null}
                  </td>
                  <td>
                    {jobHistory.endDate ? (
                      <TextFormat
                        type="date"
                        value={jobHistory.endDate}
                        format={APP_DATE_FORMAT}
                      />
                    ) : null}
                  </td>
                  <td>{jobHistory.language}</td>
                  <td>
                    {jobHistory.job ? (
                      <Link to={`/job/${jobHistory.job.id}`}>
                        {jobHistory.job.id}
                      </Link>
                    ) : (
                      ""
                    )}
                  </td>
                  <td>
                    {jobHistory.department ? (
                      <Link to={`/department/${jobHistory.department.id}`}>
                        {jobHistory.department.id}
                      </Link>
                    ) : (
                      ""
                    )}
                  </td>
                  <td>
                    {jobHistory.employee ? (
                      <Link to={`/employee/${jobHistory.employee.id}`}>
                        {jobHistory.employee.id}
                      </Link>
                    ) : (
                      ""
                    )}
                  </td>
                  <td className="text-end">
                    <div className="btn-group flex-btn-group-container">
                      <Button
                        tag={Link}
                        to={`/job-history/${jobHistory.id}`}
                        color="info"
                        size="sm"
                        data-cy="entityDetailsButton"
                      >
                        <FontAwesomeIcon icon="eye" />{" "}
                        <span className="d-none d-md-inline">View</span>
                      </Button>
                      <Button
                        tag={Link}
                        to={`/job-history/${jobHistory.id}/edit`}
                        color="primary"
                        size="sm"
                        data-cy="entityEditButton"
                      >
                        <FontAwesomeIcon icon="pencil-alt" />{" "}
                        <span className="d-none d-md-inline">Edit</span>
                      </Button>
                      <Button
                        tag={Link}
                        to={`/job-history/${jobHistory.id}/delete`}
                        color="danger"
                        size="sm"
                        data-cy="entityDeleteButton"
                      >
                        <FontAwesomeIcon icon="trash" />{" "}
                        <span className="d-none d-md-inline">Delete</span>
                      </Button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        ) : (
          !loading && (
            <div className="alert alert-warning">No Job Histories found</div>
          )
        )}
      </div>
    </div>
  );
};

export default JobHistory;
