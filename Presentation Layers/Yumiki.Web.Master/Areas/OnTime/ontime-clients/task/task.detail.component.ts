import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Guid } from '../common/guid.js';
import { Constants } from '../common/constants.js';

import { Task } from '../models/task.model.js';

import { TaskService } from './task.service.js';

declare var yumiki: any;

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Task/Detail',
})
export class TaskDetailComponent implements OnInit {
    task?: Task;
    title: string;

    constructor(
        private route: ActivatedRoute,
        private service: TaskService
    ) {
        this.task = new Task();
    }

    ngOnInit() {
        const id: string = this.route.snapshot.paramMap.get('id') as string;

        if (!id) {
            this.title = "Create New Task"

            this.task = new Task();
            this.task.ID = Guid.empty;
            this.task.PhaseID = '';
            this.task.ProjectID = '';
            this.task.AssignedUserID = '';
            this.task.Status = '';
            this.task.Priority = '';
            this.task.IsActive = true;
        }
        else {
            yumiki.message.displayLoadingDialog(true);

            this.service.getTask(id).subscribe(
                result => {
                    this.task = result;

                    this.title = "Edit Task: " + this.task.TaskName;

                    yumiki.message.displayLoadingDialog(false);
                },
                err => {
                    yumiki.message.displayLoadingDialog(false);
                    yumiki.message.clientMessage(err, '', Constants.ErrorType);
                });
        }
    }

    onSave() {
        yumiki.message.displayLoadingDialog(true);

        this.service.saveTask(this.task as Task).subscribe(
            () => {
                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}
