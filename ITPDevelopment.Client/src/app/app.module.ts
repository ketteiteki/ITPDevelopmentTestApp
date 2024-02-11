import {HttpClientModule} from "@angular/common/http";
import {AppComponent} from "./app.component";
import {LayoutComponent} from "./pages/layout/layout.component";
import {BrowserModule} from "@angular/platform-browser";
import {CommonModule} from "@angular/common";
import {RouterOutlet} from "@angular/router";
import {NgModule} from "@angular/core";
import { TaskTableComponent } from './components/task-table/task-table.component';
import { ProjectTableComponent } from './components/project-table/project-table.component';
import {AppRoutingModule} from "./app.routes";
import {ContextMenuModule} from "primeng/contextmenu";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {FormsModule} from "@angular/forms";

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
  bootstrap: [AppComponent]
})
export class AppModule { }