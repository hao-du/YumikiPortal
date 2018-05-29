import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { TaskListComponent } from './task.list.component.js';

import { TaskService } from './task.service.js';

@NgModule({
    imports: [BrowserModule, HttpClientModule, FormsModule],
    declarations: [TaskListComponent],
    bootstrap: [TaskListComponent],
    providers: [TaskService]
})
export class AppModule { }
