import axios from "axios";
import {
  createAsyncThunk,
  isFulfilled,
  isPending,
  isRejected,
} from "@reduxjs/toolkit";

import { cleanEntity } from "app/shared/util/entity-utils";
import {
  IQueryParams,
  createEntitySlice,
  EntityState,
  serializeAxiosError,
} from "app/shared/reducers/reducer.utils";
import { IJobHistory, defaultValue } from "app/shared/model/job-history.model";

const initialState: EntityState<IJobHistory> = {
  loading: false,
  errorMessage: null,
  entities: [],
  entity: defaultValue,
  updating: false,
  updateSuccess: false,
};

const apiUrl = "api/job-histories";

// Actions

export const getEntities = createAsyncThunk(
  "jobHistory/fetch_entity_list",
  async ({ page, size, sort }: IQueryParams) => {
    const requestUrl = `${apiUrl}?cacheBuster=${new Date().getTime()}`;
    return axios.get<IJobHistory[]>(requestUrl);
  }
);

export const getEntity = createAsyncThunk(
  "jobHistory/fetch_entity",
  async (id: string | number) => {
    const requestUrl = `${apiUrl}/${id}`;
    return axios.get<IJobHistory>(requestUrl);
  },
  { serializeError: serializeAxiosError }
);

export const createEntity = createAsyncThunk(
  "jobHistory/create_entity",
  async (entity: IJobHistory, thunkAPI) => {
    const result = await axios.post<IJobHistory>(apiUrl, cleanEntity(entity));
    thunkAPI.dispatch(getEntities({}));
    return result;
  },
  { serializeError: serializeAxiosError }
);

export const updateEntity = createAsyncThunk(
  "jobHistory/update_entity",
  async (entity: IJobHistory, thunkAPI) => {
    const result = await axios.put<IJobHistory>(
      `${apiUrl}/${entity.id}`,
      cleanEntity(entity)
    );
    thunkAPI.dispatch(getEntities({}));
    return result;
  },
  { serializeError: serializeAxiosError }
);

export const partialUpdateEntity = createAsyncThunk(
  "jobHistory/partial_update_entity",
  async (entity: IJobHistory, thunkAPI) => {
    const result = await axios.patch<IJobHistory>(
      `${apiUrl}/${entity.id}`,
      cleanEntity(entity)
    );
    thunkAPI.dispatch(getEntities({}));
    return result;
  },
  { serializeError: serializeAxiosError }
);

export const deleteEntity = createAsyncThunk(
  "jobHistory/delete_entity",
  async (id: string | number, thunkAPI) => {
    const requestUrl = `${apiUrl}/${id}`;
    const result = await axios.delete<IJobHistory>(requestUrl);
    thunkAPI.dispatch(getEntities({}));
    return result;
  },
  { serializeError: serializeAxiosError }
);

// slice

export const JobHistorySlice = createEntitySlice({
  name: "jobHistory",
  initialState,
  extraReducers(builder) {
    builder
      .addCase(getEntity.fulfilled, (state, action) => {
        state.loading = false;
        state.entity = action.payload.data;
      })
      .addCase(deleteEntity.fulfilled, (state) => {
        state.updating = false;
        state.updateSuccess = true;
        state.entity = {};
      })
      .addMatcher(isFulfilled(getEntities), (state, action) => {
        const { data } = action.payload;

        return {
          ...state,
          loading: false,
          entities: data,
        };
      })
      .addMatcher(
        isFulfilled(createEntity, updateEntity, partialUpdateEntity),
        (state, action) => {
          state.updating = false;
          state.loading = false;
          state.updateSuccess = true;
          state.entity = action.payload.data;
        }
      )
      .addMatcher(isPending(getEntities, getEntity), (state) => {
        state.errorMessage = null;
        state.updateSuccess = false;
        state.loading = true;
      })
      .addMatcher(
        isPending(
          createEntity,
          updateEntity,
          partialUpdateEntity,
          deleteEntity
        ),
        (state) => {
          state.errorMessage = null;
          state.updateSuccess = false;
          state.updating = true;
        }
      );
  },
});

export const { reset } = JobHistorySlice.actions;

// Reducer
export default JobHistorySlice.reducer;
