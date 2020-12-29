import { ElementRef } from '@angular/core';
import { Disposable } from './disposable';

export interface ResizeObservingStrategy {
  observe(elementRef: ElementRef<HTMLElement>, callback: () => void): Disposable;
}
