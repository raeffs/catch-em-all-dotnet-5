import { Sort } from './sort';

export interface PageRequest<T> {
  page: number;
  size: number;
  sort?: Sort<T>;
}
