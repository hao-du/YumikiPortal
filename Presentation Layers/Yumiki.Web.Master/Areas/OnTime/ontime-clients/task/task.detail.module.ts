import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { TaskDetailComponent } from './task.detail.component.js';

import { TaskService } from './task.service.js';

@NgModule({
    imports: [BrowserModule, HttpClientModule, FormsModule],
    declarations: [TaskDetailComponent],
    bootstrap: [TaskDetailComponent],
    providers: [TaskService]
})
export class AppModule { }
