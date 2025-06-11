import { WritableSignal } from "@angular/core";
import { BehaviorSubject, Subject } from "rxjs";

export class StatePromise<TResult>{

    private _isFinished = new BehaviorSubject<boolean>(false);

    public get isFinished() : boolean { return this._isFinished.getValue(); }
    
    constructor(private promise: Promise<TResult>){
        this.promise.finally(() => this._isFinished.next(true));
    }

    async awaitSafe(): Promise<{ isSuccess: boolean, result?: TResult, error?:string}> {
    try {
        const result = await this.promise;
        return { isSuccess: true, result };
    } catch (error : any) {
        return { isSuccess: false, error: error?.error?.message };
    }
}

    registerLoadingSignal(loadingSignal: WritableSignal<boolean>){
        this._isFinished.subscribe({
            next: (isFinished) => loadingSignal.set(!isFinished)
        })
    }

    registerLoadingSubject(loadingSubject: Subject<boolean>){
        this._isFinished.subscribe({
            next: (isFinished) => loadingSubject.next(!isFinished)
        })
    }
}