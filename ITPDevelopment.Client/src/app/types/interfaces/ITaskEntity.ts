import {IProjectEntity} from "./IProjectEntity";


export interface ITaskEntity {
  id: string;
  taskName: string;
  description: string;
  projectId: string;
  project: IProjectEntity;
  startDate: string | null;
  endDate: string | null;
  createDate: string;
  updateDate: string;
}