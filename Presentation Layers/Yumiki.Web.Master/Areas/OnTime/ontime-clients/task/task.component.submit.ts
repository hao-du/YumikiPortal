import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Guid } from '../common/guid.js';
import { Constants } from '../common/constants.js';

import { Task } from '../models/task.model.js';

import { TaskService } from './task.service.js';

declare var yumiki: any;

@Component({
    selector: 'task-submit',
    templateUrl: '/Apps/OnTime/Task/Submit',
})
export class TaskSubmitComponent implements OnInit {
    @Input() task?: Task;

    constructor(
        private route: ActivatedRoute,
        private service: TaskService,
        private location: Location
    ) {
        this.task = new Task();
    }

    ngOnInit() {
        const id: string = this.route.snapshot.paramMap.get('id') as string;

        if (!id) {
            this.task = new Task();
            this.task.ID = Guid.empty;
            this.task.PhaseID = '';
            this.task.ProjectID = '';
            this.task.AssignedUserID = '';
            this.task.Status = '';
            this.task.IsActive = true;
        }
        else {
            yumiki.message.displayLoadingDialog(true);

            this.service.getTask(id).subscribe(
                result => {
                    this.task = result;

                    yumiki.message.displayLoadingDialog(false);
                },
                err => {
                    yumiki.message.displayLoadingDialog(false);
                    yumiki.message.clientMessage(err, '', Constants.ErrorType);
                });
        }
    }

    onClose() {
        this.location.back();
        return false;
    }

    onSave() {
        yumiki.message.displayLoadingDialog(true);

        this.service.saveTask(this.task as Task).subscribe(
            () => {
                this.onClose();

                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}
