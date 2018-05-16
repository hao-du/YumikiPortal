import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { BsDatepickerModule } from 'ngx-bootstrap';

import { TaskListComponent } from './task.component.list.js';
import { TaskSubmitComponent } from './task.component.submit.js';

import { TaskService } from './task.service.js';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, BsDatepickerModule.forRoot()],
    declarations: [TaskListComponent, TaskSubmitComponent],
    bootstrap: [TaskListComponent],
    providers: [TaskService]
})
export class AppModule { }
