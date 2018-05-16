import { Component, OnInit } from '@angular/core';

import { Guid } from '../common/guid.js'
import { Constants } from '../common/constants.js'
import { Task } from '../models/task.model.js'

import { TaskService } from './task.service.js';

declare var yumiki: any;

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Task/List',
})
export class TaskListComponent implements OnInit {
    tasks: Task[];
    selectedTask: Task;

    constructor(private service: TaskService) {
    }

    ngOnInit() {
        this.getTasks();
    }

    onSelect(task: Task) {
        if (!task) {
            task = new Task();
            task.ID = Guid.empty;
            task.PhaseID = '';
            task.ProjectID = '';
            task.AssignedUserID = '';
            task.Status = '';
            task.IsActive = true;

            this.selectedTask = task;
        }
        else {
            if (task && task.ID != Guid.empty) {
                yumiki.message.displayLoadingDialog(true);

                this.service.getTask(task.ID).subscribe(
                    result => {
                        this.selectedTask = task = result;

                        yumiki.message.displayLoadingDialog(false);
                    },
                    err => {
                        yumiki.message.displayLoadingDialog(false);
                        yumiki.message.clientMessage(err, '', Constants.ErrorType);
                    });
            }
        }
    }

    onRefreshData(message: string) {
        if (message == 'ok') {
            this.getTasks();
        }
    }

    getTasks() {
        yumiki.message.displayLoadingDialog(true);

        this.service.getTasks().subscribe(
            tasks => {
                this.tasks = tasks;

                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}
