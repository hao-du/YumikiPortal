import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Constants } from '../common/constants.js'
import { Task } from '../models/task.model.js';

import { TaskService } from './task.service.js';

declare var yumiki: any;

@Component({
    selector: 'task-submit',
    templateUrl: '/Apps/OnTime/Task/Submit',
})
export class TaskSubmitComponent implements OnInit {
    @Input() task?: Task;
    @Output() messageEvent = new EventEmitter<string>();

    constructor(private service: TaskService) {
    }

    ngOnInit() {
    }

    onClose() {
        this.task = undefined;
    }

    onSave() {
        yumiki.message.displayLoadingDialog(true);

        this.service.saveTask(this.task as Task).subscribe(
            () => {
                //Emit message to List Compoment to refresh data
                this.messageEvent.emit('ok');

                this.onClose();

                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}
