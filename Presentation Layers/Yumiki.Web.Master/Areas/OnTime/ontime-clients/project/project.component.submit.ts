import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Project } from '../models/project.model.js';

import { ProjectService } from './project.service.js';

declare var yumiki: any;

@Component({
    selector: 'project-submit',
    templateUrl: '/Apps/OnTime/Project/Submit',
})
export class ProjectSubmitComponent implements OnInit {
    @Input() project?: Project;
    @Output() messageEvent = new EventEmitter<string>();

    constructor(private service: ProjectService) {
    }

    ngOnInit() {
    }

    onClose() {
        this.project = undefined;
    }

    onSave()
    {
        yumiki.message.displayLoadingDialog(true);

        this.service.saveProject(this.project as Project).subscribe(result => {
            console.log(result);

            //Emit message to List Compoment to refresh data
            this.messageEvent.emit('ok');

            this.onClose();

            yumiki.message.displayLoadingDialog(false);
        });
    }
}
