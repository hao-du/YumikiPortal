import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
//import { Location } from '@angular/common';

import { Constants } from '../common/constants.js'
import { User } from '../models/user.model.js'
import { Project } from '../models/project.model.js'

import { AssignmentService } from './assignment.service.js';

declare var yumiki: any;

@Component({
    selector: 'assignment',
    templateUrl: '/Apps/OnTime/UserAssignment/AssignmentList',
})
export class AssignmentComponent implements OnInit {
    user: User;

    constructor(
        private route: ActivatedRoute,
        private service: AssignmentService
    ) {
        //Inittial user instance
        this.user = new User();
    }

    ngOnInit() {
        this.getAssignments();
    }

    onChange(project: Project) {
        this.service.saveProjectAssignments(this.user.ID, project.ID, project.IsAssigned).subscribe(
            () => {},
            err => {
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }

    getAssignments() {
        const id: string = this.route.snapshot.paramMap.get('id') as string;
        this.service.getAssignments(id).subscribe(
            result => {
                this.user = result;

                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}
