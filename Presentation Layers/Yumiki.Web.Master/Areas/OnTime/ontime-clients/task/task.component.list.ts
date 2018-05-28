import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { TaskPaging } from '../models/taskpaging.model.js'

import { TaskService } from './task.service.js';
import { Constants } from "../common/constants.js";

declare var yumiki: any;

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Task/List',
})
export class TaskListComponent implements OnInit {
    title: string;
    tasks: TaskPaging;

    constructor(
        private route: ActivatedRoute,
        private service: TaskService,
        private location: Location
    ) {
    }

    ngOnInit() {
        this.getTasks();
    }

    onClose() {
        this.location.back();
        return false;
    }

    getTasks() {
        const type: string = this.route.snapshot.paramMap.get('type') as string;

        switch (+type) {
            case 1:
                this.title = "My Assigned Tasks";
                break;
            case 2:
                this.title = "My Created Tasks";
                break;
            case 3:
                this.title = "Recent Tasks";
                break;
            case 4:
                this.title = "Unassigned Tasks";
                break;
        }

        yumiki.message.displayLoadingDialog(true);

        this.service.getTasks(+type, '', '').subscribe(
            result => {
                this.tasks = result;

                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}
