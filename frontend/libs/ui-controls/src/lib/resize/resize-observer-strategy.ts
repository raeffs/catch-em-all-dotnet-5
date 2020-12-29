import { ElementRef } from '@angular/core';
import { Disposable } from './disposable';
import { ResizeObservingStrategy } from './resize-observing-strategy';

export class ResizeObserverStrategy implements ResizeObservingStrategy {
  public observe(elementRef: ElementRef<HTMLElement>, callback: () => void): Disposable {
    const observer = new ResizeObserver(callback);
    observer.observe(elementRef.nativeElement);
    return () => {
      observer.unobserve(elementRef.nativeElement);
    };
  }
}

export function supportsObserver(): boolean {
  return !!(window as any).ResizeObserver;
}

/** Experimental API */
declare class ResizeObserver {
  constructor(callback: ResizeObserverCallback);
  public disconnect: () => void;
  public observe: (target: Element, options?: ResizeObserverObserveOptions) => void;
  public unobserve: (target: Element) => void;
}

declare interface ResizeObserverObserveOptions {
  box?: 'content-box' | 'border-box';
}

declare type ResizeObserverCallback = (entries: ResizeObserverEntry[], observer: ResizeObserver) => void;

declare interface ResizeObserverEntry {
  readonly borderBoxSize: ResizeObserverEntryBoxSize;
  readonly contentBoxSize: ResizeObserverEntryBoxSize;
  readonly contentRect: DOMRectReadOnly;
  readonly target: Element;
}

declare interface ResizeObserverEntryBoxSize {
  blockSize: number;
  inlineSize: number;
}
