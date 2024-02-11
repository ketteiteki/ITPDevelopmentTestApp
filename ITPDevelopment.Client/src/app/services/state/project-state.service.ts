import { Injectable } from '@angular/core';
import {BehaviorSubject, firstValueFrom} from "rxjs";
import {IProjectEntity} from "../../types/interfaces/IProjectEntity";
import {ProjectApiService} from "../api/project-api.service";
import {CreateProjectRequest} from "../../types/requests/CreateProjectRequest";

@Injectable({
  providedIn: 'root'
})
export class ProjectStateService {
  public projects$: BehaviorSubject<IProjectEntity[]> = new BehaviorSubject<IProjectEntity[]>([]);

  constructor(
    private _projectApiService: ProjectApiService
  ) { }

  public async getProjectListAsync() {
    const getProjectList$ = this._projectApiService.getProjectList();
    const getProjectListResult = await firstValueFrom(getProjectList$);

    this.projects$.next(getProjectListResult.response)

    return getProjectListResult;
  }

  public async createProjectAsync(request: CreateProjectRequest) {
    const postCreateProject$ = this._projectApiService.postCreateProject(request);
    const postCreateProjectResult = await firstValueFrom(postCreateProject$);

    const projects = this.projects$.getValue();
    projects.push(postCreateProjectResult.response);

    this.projects$.next(projects);

    return postCreateProjectResult;
  }
}
