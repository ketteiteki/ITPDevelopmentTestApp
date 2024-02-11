import {HttpClient, HttpClientModule} from "@angular/common/http";
import {AppComponent} from "./app.component";
import {LayoutComponent} from "./pages/layout/layout.component";
import {BrowserModule} from "@angular/platform-browser";
import {CommonModule} from "@angular/common";
import {RouterOutlet} from "@angular/router";
import {APP_INITIALIZER, NgModule} from "@angular/core";
import { TaskTableComponent } from './components/task-table/task-table.component';
import { ProjectTableComponent } from './components/project-table/project-table.component';
import {AppRoutingModule} from "./app.routes";
import {ContextMenuModule} from "primeng/contextmenu";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {FormsModule} from "@angular/forms";
import {Observable} from "rxjs";

interface IConfig {
  baseUrl: string
}

function initializeAppFactory(httpClient: HttpClient): () => Observable<any> {
  const configUrl = 'assets/config/config.json';

  return () => {
    const result = httpClient.get<IConfig>(configUrl)

    result.subscribe(x => localStorage.setItem("serverUrl", x.baseUrl));

    return result;
  };
}

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    TaskTableComponent,
    ProjectTableComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CommonModule,
    FormsModule,
    RouterOutlet,
    ContextMenuModule,
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initializeAppFactory,
      deps: [HttpClient],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }