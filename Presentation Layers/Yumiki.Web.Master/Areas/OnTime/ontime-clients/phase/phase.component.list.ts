import { Component, OnInit } from '@angular/core';

import { Guid } from '../common/guid.js'
import { Constants } from '../common/constants.js'
import { Phase } from '../models/phase.model.js'

import { PhaseService } from './phase.service.js';

declare var yumiki: any;

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Phase/List',
})
export class PhaseListComponent implements OnInit {
    phases: Phase[];
    selectedPhase: Phase;
    showActiveList: boolean;

    constructor(private service: PhaseService) {
        this.showActiveList = true;
    }

    ngOnInit() {
        this.getPhases();
    }

    onSelect(phase: Phase) {
        if (!phase) {
            phase = new Phase();
            phase.ID = Guid.empty;
            phase.IsActive = true;
            phase.Status = "";

            this.selectedPhase = phase;
        }
        else {
            if (phase && phase.ID != Guid.empty) {
                yumiki.message.displayLoadingDialog(true);

                this.service.getPhase(phase.ID).subscribe(
                    result => {
                        this.selectedPhase = phase = result;

                        yumiki.message.displayLoadingDialog(false);
                    },
                    err => {
                        yumiki.message.displayLoadingDialog(false);
                        yumiki.message.clientMessage(err, '', Constants.ErrorType);
                    });
            }
        }
    }

    onShowList() {
        this.showActiveList = !this.showActiveList;
        this.getPhases();
    }

    onRefreshData(message: string) {
        if (message == 'ok') {
            this.getPhases();
        }
    }

    getPhases() {
        yumiki.message.displayLoadingDialog(true);

        this.service.getPhases(this.showActiveList).subscribe(
            result => {
                this.phases = result;

                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}
