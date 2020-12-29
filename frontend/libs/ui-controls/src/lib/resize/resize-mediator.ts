import { Injectable } from '@angular/core';
import { asyncScheduler, Observable, Subject } from 'rxjs';
import { throttleTime } from 'rxjs/operators';

/**
 * Mediator used to trigger reevaluation of the size of an element.
 */
@Injectable()
export class ResizeMediator {
  private readonly reevaluationTrigger: Subject<void> = new Subject<void>();

  public readonly reevaluate: Observable<void> = this.reevaluationTrigger
    .asObservable()
    .pipe(throttleTime(200, asyncScheduler, { leading: true, trailing: true }));

  public triggerReevaluation(): void {
    this.reevaluationTrigger.next();
  }
}
