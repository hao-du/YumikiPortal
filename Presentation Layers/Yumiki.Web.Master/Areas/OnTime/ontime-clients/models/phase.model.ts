import { OnTimeBase } from './ontimebase.model.js';

export class Phase extends OnTimeBase {
    PhaseName: string;
    EstimatedStartDate: string;
    EstimatedEndDate: string;
    ActualStartDate: string;
    ActualEndDate: string;
    ReleaseVersion: string;
    Status: string;
    StatusUI: string;
    //For User Assignment Purpose to check if user is assigned to specific phase
    IsAssigned: boolean;
}
