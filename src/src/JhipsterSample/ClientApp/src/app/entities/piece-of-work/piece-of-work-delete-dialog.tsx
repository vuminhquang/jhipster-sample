import React, { useEffect, useState } from "react";
import { RouteComponentProps } from "react-router-dom";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from "reactstrap";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { useAppDispatch, useAppSelector } from "app/config/store";
import { getEntity, deleteEntity } from "./piece-of-work.reducer";

export const PieceOfWorkDeleteDialog = (
  props: RouteComponentProps<{ id: string }>
) => {
  const [loadModal, setLoadModal] = useState(false);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getEntity(props.match.params.id));
    setLoadModal(true);
  }, []);

  const pieceOfWorkEntity = useAppSelector((state) => state.pieceOfWork.entity);
  const updateSuccess = useAppSelector(
    (state) => state.pieceOfWork.updateSuccess
  );

  const handleClose = () => {
    props.history.push("/piece-of-work");
  };

  useEffect(() => {
    if (updateSuccess && loadModal) {
      handleClose();
      setLoadModal(false);
    }
  }, [updateSuccess]);

  const confirmDelete = () => {
    dispatch(deleteEntity(pieceOfWorkEntity.id));
  };

  return (
    <Modal isOpen toggle={handleClose}>
      <ModalHeader
        toggle={handleClose}
        data-cy="pieceOfWorkDeleteDialogHeading"
      >
        Confirm delete operation
      </ModalHeader>
      <ModalBody id="jhipsterSampleApp.pieceOfWork.delete.question">
        Are you sure you want to delete this PieceOfWork?
      </ModalBody>
      <ModalFooter>
        <Button color="secondary" onClick={handleClose}>
          <FontAwesomeIcon icon="ban" />
          &nbsp; Cancel
        </Button>
        <Button
          id="jhi-confirm-delete-pieceOfWork"
          data-cy="entityConfirmDeleteButton"
          color="danger"
          onClick={confirmDelete}
        >
          <FontAwesomeIcon icon="trash" />
          &nbsp; Delete
        </Button>
      </ModalFooter>
    </Modal>
  );
};

export default PieceOfWorkDeleteDialog;
