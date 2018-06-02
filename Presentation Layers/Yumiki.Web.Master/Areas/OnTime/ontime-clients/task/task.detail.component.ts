import { Component, OnInit } from '@angular/core';

import { Guid } from '../common/guid.js';
import { Constants } from '../common/constants.js';

import { Task } from '../models/task.model.js';

import { TaskService } from './task.service.js';

declare var yumiki: any;

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Task/DetailContent',
})
export class TaskDetailComponent implements OnInit {
    task?: Task;
    title: string;

    constructor(
        private service: TaskService
    ) {
        this.task = new Task();
    }

    ngOnInit() {
        const id: string = yumiki.url.getURLParameter('id');
        const phaseID: string = yumiki.url.getURLParameter('phaseID');
        const projectID: string = yumiki.url.getURLParameter('projectID');

        if (!id || id == Guid.empty) {
            this.title = "Create New Task"

            this.task = new Task();
            this.task.ID = Guid.empty;
            this.task.PhaseID = phaseID;
            this.task.ProjectID = projectID;
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

                    this.title = "Edit Task: " + this.task.TaskNumber;

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
                window.location.href = "/Detail/else";

                this.onBack();
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }

    onBack() {
        if (this.task) {
            window.location.href = "Index?phaseID=" + this.task.PhaseID + "&projectID=" + this.task.ProjectID;
        }
    }
}
