import { OnTimeBase } from './ontimebase.model.js';

export class Phase extends OnTimeBase {
    PhaseName: string;
    EstimatedStartDate: Date;
    EstimatedEndDate: Date;
    ActualStartDate: Date;
    ActualEndDate: Date;
    ReleaseVersion: string;
    Status: string;

    //For User Assignment Purpose to check if user is assigned to specific phase
    IsAssigned: boolean;
}
