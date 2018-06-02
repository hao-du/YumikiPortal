import { Component, OnInit } from '@angular/core';

import { Constants } from '../common/constants.js'
import { User } from '../models/user.model.js'

import { AssignmentService } from './assignment.service.js';

declare var yumiki: any;

@Component({
    selector: 'user',
    templateUrl: '/Apps/OnTime/UserAssignment/UserList',
})
export class UserComponent implements OnInit {
    users: User[];

    constructor(private service: AssignmentService) {
    }

    ngOnInit() {
        this.getUsers();
    }

    getUsers() {
        yumiki.message.displayLoadingDialog(true);

        this.service.getUsers().subscribe(
            result => {
                this.users = result;

                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}
