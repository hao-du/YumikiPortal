import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { BsDatepickerModule } from 'ngx-bootstrap';

import { TaskDashboardComponent } from './task.component.dashboard.js';
import { TaskListComponent } from './task.component.list.js';
import { TaskSubmitComponent } from './task.component.submit.js';
import { TaskLandingComponent } from './task.component.landing.js';

import { TaskService } from './task.service.js';

import { TaskRouting } from './task.rounting.js';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, BsDatepickerModule.forRoot(), TaskRouting],
    declarations: [TaskLandingComponent, TaskDashboardComponent, TaskListComponent, TaskSubmitComponent],
    bootstrap: [TaskLandingComponent],
    providers: [TaskService]
})
export class AppModule { }
