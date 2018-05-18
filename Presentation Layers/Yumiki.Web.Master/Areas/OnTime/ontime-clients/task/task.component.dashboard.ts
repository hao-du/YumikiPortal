import { Component, OnInit } from '@angular/core';

import { Constants } from '../common/constants.js'
import { Task } from '../models/task.model.js'
import { TaskDashboard } from '../models/taskdashboard.model.js'

import { TaskService } from './task.service.js';

declare var yumiki: any;

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Task/Dashboard',
})
export class TaskDashboardComponent implements OnInit {
    dashboard: TaskDashboard;
    selectedTask: Task;

    constructor(private service: TaskService) {
    }

    ngOnInit() {
        this.dashboard = new TaskDashboard();
        this.getDashboard();
    }

    getDashboard() {
        yumiki.message.displayLoadingDialog(true);

        this.service.getTaskDashboard().subscribe(
            result => {
                this.dashboard = result;

                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}