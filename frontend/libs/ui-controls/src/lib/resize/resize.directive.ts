import { AfterViewInit, Directive, ElementRef, OnDestroy, Optional, Output, Renderer2 } from '@angular/core';
import { asyncScheduler, noop, Observable } from 'rxjs';
import { filter, observeOn, scan } from 'rxjs/operators';
import { Disposable } from './disposable';
import { ResizeMediator } from './resize-mediator';
import { ResizeObserverStrategy } from './resize-observer-strategy';
import { ResizeObservingStrategy } from './resize-observing-strategy';

/**
 * Event emitted by the `ResizeDirective`.
 */
export interface ResizeEvent {
  /** Whether it is the first event emitted or not. */
  readonly isFirstChange: boolean;
  /** The current height of the element. */
  readonly currentHeight: number;
  /** The current width of the element. */
  readonly currentWidth: number;
  /** The change of the height since the last time emitted. */
  readonly deltaHeight: number;
  /** The change of the width since the last time emitted. */
  readonly deltaWidth: number;
}

/**
 * Directive that can be used to react on the size of an element.
 */
@Directive({
  selector: '[ceaResize]',
})
export class ResizeDirective implements AfterViewInit, OnDestroy {
  private readonly strategy: ResizeObservingStrategy;

  private dispose: Disposable = noop;

  @Output('ceaResize')
  public get resize(): Observable<ResizeEvent> {
    return this.mediator.reevaluate.pipe(
      // we use the async scheduler because typically event handlers reacting on this event
      // will do changes, and as the first value is triggered from the ngAfterViewInit hook
      // this would result in changes during the change detection phase when the default
      // scheduler would be used. additionally we want re-rendering to be done after changes
      // happen before we evaluate the new dimensions
      observeOn(asyncScheduler),
      scan(previous => this.createEventArguments(previous), (null as unknown) as ResizeEvent),
      filter(e => !!e.deltaHeight || !!e.deltaWidth)
    );
  }

  constructor(
    private readonly elementRef: ElementRef<HTMLElement>,
    private readonly renderer: Renderer2,
    @Optional() private readonly mediator: ResizeMediator
  ) {
    this.mediator = mediator || new ResizeMediator();
    this.strategy = new ResizeObserverStrategy();
  }

  public ngAfterViewInit(): void {
    this.dispose = this.strategy.observe(this.elementRef, () => this.mediator.triggerReevaluation());
    this.mediator.triggerReevaluation();
  }

  public ngOnDestroy(): void {
    this.dispose();
  }

  private createEventArguments(previous?: ResizeEvent): ResizeEvent {
    const element = this.elementRef.nativeElement;
    const currentHeight = element.clientHeight;
    const currentWidth = element.clientWidth;
    const isFirstChange = !previous;
    return {
      isFirstChange,
      currentHeight,
      currentWidth,
      deltaHeight: !previous ? currentHeight : currentHeight - previous.currentHeight,
      deltaWidth: !previous ? currentWidth : currentWidth - previous.currentWidth,
    };
  }
}
