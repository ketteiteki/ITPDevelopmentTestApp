import { Injectable } from '@angular/core';
import {IResult} from "../../types/interfaces/IResult";
import {HttpClient} from "@angular/common/http";
import {ITaskEntity} from "../../types/interfaces/ITaskEntity";
import {CreateTaskRequest} from "../../types/requests/CreateTaskRequest";
import {UpdateTaskDescriptionRequest} from "../../types/requests/UpdateTaskDescriptionRequest";
import {ConfigService} from "../common/config.service";

@Injectable({
  providedIn: 'root'
})
export class TaskApiService {
  private readonly baseUrl: string = "https://localhost:7031/";

  constructor(
    private _httpClient: HttpClient,
    private _configService: ConfigService
  ) {
    this.baseUrl = _configService.getServerUrl();
  }

  // GET /Task
  public getTaskList(offset: number, limit: number) {
    return this._httpClient.get<IResult<ITaskEntity[]>>(
      this.baseUrl + `Task?offset=${offset}&limit=${limit}`
    );
  }

  // POST /Task
  public postCreateTask(request: CreateTaskRequest) {
    return this._httpClient.post<IResult<ITaskEntity>>(
      this.baseUrl + `Task`, request
    );
  }

  // POST /Task/Start
  public postStartTask(taskId: string) {
    return this._httpClient.post<IResult<ITaskEntity>>(
      this.baseUrl + `Task/Start/${taskId}`, {}
    );
  }

  // POST /Task/End
  public postEndTask(taskId: string) {
    return this._httpClient.post<IResult<ITaskEntity>>(
      this.baseUrl + `Task/End/${taskId}`, {}
    );
  }

  // PUT /Task
  public putUpdateTask(request: UpdateTaskDescriptionRequest) {
    return this._httpClient.put<IResult<ITaskEntity>>(
      this.baseUrl + `Task`, request
    );
  }
}
