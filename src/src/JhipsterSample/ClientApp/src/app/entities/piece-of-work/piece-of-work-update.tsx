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

import { IJob } from "app/shared/model/job.model";
import { getEntities as getJobs } from "app/entities/job/job.reducer";
import { IPieceOfWork } from "app/shared/model/piece-of-work.model";
import {
  getEntity,
  updateEntity,
  createEntity,
  reset,
} from "./piece-of-work.reducer";

export const PieceOfWorkUpdate = (
  props: RouteComponentProps<{ id: string }>
) => {
  const dispatch = useAppDispatch();

  const [isNew] = useState(!props.match.params || !props.match.params.id);

  const jobs = useAppSelector((state) => state.job.entities);
  const pieceOfWorkEntity = useAppSelector((state) => state.pieceOfWork.entity);
  const loading = useAppSelector((state) => state.pieceOfWork.loading);
  const updating = useAppSelector((state) => state.pieceOfWork.updating);
  const updateSuccess = useAppSelector(
    (state) => state.pieceOfWork.updateSuccess
  );
  const handleClose = () => {
    props.history.push("/piece-of-work");
  };

  useEffect(() => {
    if (isNew) {
      dispatch(reset());
    } else {
      dispatch(getEntity(props.match.params.id));
    }

    dispatch(getJobs({}));
  }, []);

  useEffect(() => {
    if (updateSuccess) {
      handleClose();
    }
  }, [updateSuccess]);

  const saveEntity = (values) => {
    const entity = {
      ...pieceOfWorkEntity,
      ...values,
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
          ...pieceOfWorkEntity,
        };

  return (
    <div>
      <Row className="justify-content-center">
        <Col md="8">
          <h2
            id="jhipsterSampleApp.pieceOfWork.home.createOrEditLabel"
            data-cy="PieceOfWorkCreateUpdateHeading"
          >
            Create or edit a PieceOfWork
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
                  id="piece-of-work-id"
                  label="ID"
                  validate={{ required: true }}
                />
              ) : null}
              <ValidatedField
                label="Title"
                id="piece-of-work-title"
                name="title"
                data-cy="title"
                type="text"
              />
              <ValidatedField
                label="Description"
                id="piece-of-work-description"
                name="description"
                data-cy="description"
                type="text"
              />
              <Button
                tag={Link}
                id="cancel-save"
                data-cy="entityCreateCancelButton"
                to="/piece-of-work"
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

export default PieceOfWorkUpdate;
