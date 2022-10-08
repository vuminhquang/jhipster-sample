import React, { useState, useEffect } from "react";
import { Link, RouteComponentProps } from "react-router-dom";
import { Button, Row, Col, FormText } from "reactstrap";
import { isNumber, ValidatedField, ValidatedForm } from "react-jhipster";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import {
  convertDateTimeFromServer,
  convertDateTimeToServer,
  displayDefaultDateTime,
} from "app/shared/util/date-utils";
import { mapIdList } from "app/shared/util/entity-utils";
import { useAppDispatch, useAppSelector } from "app/config/store";

import { IPieceOfWork } from "app/shared/model/piece-of-work.model";
import { getEntities as getPieceOfWorks } from "app/entities/piece-of-work/piece-of-work.reducer";
import { IEmployee } from "app/shared/model/employee.model";
import { getEntities as getEmployees } from "app/entities/employee/employee.reducer";
import { IJob } from "app/shared/model/job.model";
import { getEntity, updateEntity, createEntity, reset } from "./job.reducer";

export const JobUpdate = (props: RouteComponentProps<{ id: string }>) => {
  const dispatch = useAppDispatch();

  const [isNew] = useState(!props.match.params || !props.match.params.id);

  const pieceOfWorks = useAppSelector((state) => state.pieceOfWork.entities);
  const employees = useAppSelector((state) => state.employee.entities);
  const jobEntity = useAppSelector((state) => state.job.entity);
  const loading = useAppSelector((state) => state.job.loading);
  const updating = useAppSelector((state) => state.job.updating);
  const updateSuccess = useAppSelector((state) => state.job.updateSuccess);
  const handleClose = () => {
    props.history.push("/job");
  };

  useEffect(() => {
    if (isNew) {
      dispatch(reset());
    } else {
      dispatch(getEntity(props.match.params.id));
    }

    dispatch(getPieceOfWorks({}));
    dispatch(getEmployees({}));
  }, []);

  useEffect(() => {
    if (updateSuccess) {
      handleClose();
    }
  }, [updateSuccess]);

  const saveEntity = (values) => {
    const entity = {
      ...jobEntity,
      ...values,
      chores: mapIdList(values.chores),
      employee: employees.find(
        (it) => it.id.toString() === values.employee.toString()
      ),
    };

    if (isNew) {
      dispatch(createEntity(entity));
    } else {
      dispatch(updateEntity(entity));
    }
  };

  const defaultValues = () =>
    isNew
      ? {}
      : {
          ...jobEntity,
          chores: jobEntity?.chores?.map((e) => e.id.toString()),
          employee: jobEntity?.employee?.id,
        };

  return (
    <div>
      <Row className="justify-content-center">
        <Col md="8">
          <h2
            id="jhipsterSampleApp.job.home.createOrEditLabel"
            data-cy="JobCreateUpdateHeading"
          >
            Create or edit a Job
          </h2>
        </Col>
      </Row>
      <Row className="justify-content-center">
        <Col md="8">
          {loading ? (
            <p>Loading...</p>
          ) : (
            <ValidatedForm
              defaultValues={defaultValues()}
              onSubmit={saveEntity}
            >
              {!isNew ? (
                <ValidatedField
                  name="id"
                  required
                  readOnly
                  id="job-id"
                  label="ID"
                  validate={{ required: true }}
                />
              ) : null}
              <ValidatedField
                label="Job Title"
                id="job-jobTitle"
                name="jobTitle"
                data-cy="jobTitle"
                type="text"
              />
              <ValidatedField
                label="Min Salary"
                id="job-minSalary"
                name="minSalary"
                data-cy="minSalary"
                type="text"
              />
              <ValidatedField
                label="Max Salary"
                id="job-maxSalary"
                name="maxSalary"
                data-cy="maxSalary"
                type="text"
              />
              <ValidatedField
                label="Chore"
                id="job-chore"
                data-cy="chore"
                type="select"
                multiple
                name="chores"
              >
                <option value="" key="0" />
                {pieceOfWorks
                  ? pieceOfWorks.map((otherEntity) => (
                      <option value={otherEntity.id} key={otherEntity.id}>
                        {otherEntity.title}
                      </option>
                    ))
                  : null}
              </ValidatedField>
              <ValidatedField
                id="job-employee"
                name="employee"
                data-cy="employee"
                label="Employee"
                type="select"
              >
                <option value="" key="0" />
                {employees
                  ? employees.map((otherEntity) => (
                      <option value={otherEntity.id} key={otherEntity.id}>
                        {otherEntity.id}
                      </option>
                    ))
                  : null}
              </ValidatedField>
              <Button
                tag={Link}
                id="cancel-save"
                data-cy="entityCreateCancelButton"
                to="/job"
                replace
                color="info"
              >
                <FontAwesomeIcon icon="arrow-left" />
                &nbsp;
                <span className="d-none d-md-inline">Back</span>
              </Button>
              &nbsp;
              <Button
                color="primary"
                id="save-entity"
                data-cy="entityCreateSaveButton"
                type="submit"
                disabled={updating}
              >
                <FontAwesomeIcon icon="save" />
                &nbsp; Save
              </Button>
            </ValidatedForm>
          )}
        </Col>
      </Row>
    </div>
  );
};

export default JobUpdate;
