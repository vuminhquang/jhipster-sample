import { IPieceOfWork } from "app/shared/model/piece-of-work.model";
import { IEmployee } from "app/shared/model/employee.model";

export interface IJob {
  id?: number;
  jobTitle?: string | null;
  minSalary?: number | null;
  maxSalary?: number | null;
  chores?: IPieceOfWork[] | null;
  employee?: IEmployee | null;
}

export const defaultValue: Readonly<IJob> = {};
