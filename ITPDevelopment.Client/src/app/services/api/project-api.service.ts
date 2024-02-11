import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IProjectEntity} from "../../types/interfaces/IProjectEntity";
import {IResult} from "../../types/interfaces/IResult";
import {CreateProjectRequest} from "../../types/requests/CreateProjectRequest";
import {ConfigService} from "../common/config.service";

@Injectable({
  providedIn: 'root'
})
export class ProjectApiService {
  private readonly baseUrl: string;

  constructor(
    private _httpClient: HttpClient,
    private _configService: ConfigService
  ) {
    this.baseUrl = _configService.getServerUrl();
  }

  // requests

  // get /Project
  public getProjectList() {
    return this._httpClient.get<IResult<IProjectEntity[]>>(
      this.baseUrl + "Project"
    );
  }

  // POST /Project
  public postCreateProject(request: CreateProjectRequest) {
    return this._httpClient.post<IResult<IProjectEntity>>(
      this.baseUrl + "Project", request
    );
  }
}
