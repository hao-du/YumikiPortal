import { Component, OnInit } from '@angular/core';

import { Guid } from '../common/guid.js';
import { Constants } from "../common/constants.js";

import { PagingTask } from '../models/task.paging.model.js'

import { TaskService } from './task.service.js';

declare var yumiki: any;

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Task/IndexContent',
})
export class TaskListComponent implements OnInit {
    view: string;
    taskModelView: PagingTask;

    constructor(private service: TaskService) {
    }

    ngOnInit() {
        this.taskModelView = new PagingTask();

        this.taskModelView.DefaultPhaseID = yumiki.url.getURLParameter('phaseID');
        this.taskModelView.DefaultProjectID = yumiki.url.getURLParameter('projectID');

        this.view = '1';

        this.getTasks(true);
    }

    onRefreshData() {
        this.getTasks(false);
    }

    getTasks(getDefaultMetadata: boolean) {
        yumiki.message.displayLoadingDialog(true);

        this.service.getTasks(getDefaultMetadata, +this.view, this.taskModelView.DefaultPhaseID, this.taskModelView.DefaultProjectID).subscribe(
            result => {
                this.taskModelView = result;
                this.taskModelView.DefaultTaskID = Guid.empty;

                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}
