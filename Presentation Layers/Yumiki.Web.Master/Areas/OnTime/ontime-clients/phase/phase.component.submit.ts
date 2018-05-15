import { Component, AfterViewChecked, Input, Output, EventEmitter } from '@angular/core';

import { Constants } from '../common/constants.js'
import { Phase } from '../models/phase.model.js';

import { PhaseService } from './phase.service.js';

declare var yumiki: any;

@Component({
    selector: 'phase-submit',
    templateUrl: '/Apps/OnTime/Phase/Submit',
})
export class PhaseSubmitComponent implements AfterViewChecked {
    @Input() phase?: Phase;
    @Output() messageEvent = new EventEmitter<string>();

    isControlInitialized: boolean = false;

    constructor(private service: PhaseService) {
    }

    ngAfterViewChecked() {
        if (!this.isControlInitialized && this.phase) {
            console.log(this);
            yumiki.ontime.initDateTimePicker();
            console.log("ngAfterContentChecked");
            this.isControlInitialized = true;
        }
    }

    onClose() {
        this.phase = undefined;
        this.isControlInitialized = false;
    }

    onSave() {
        yumiki.message.displayLoadingDialog(true);
        console.log(this.phase);
        this.service.savePhase(this.phase as Phase).subscribe(
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
