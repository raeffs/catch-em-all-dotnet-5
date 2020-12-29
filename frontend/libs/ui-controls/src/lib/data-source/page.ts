export interface Page<T> {
  items: T[];
  totalItems: number;
  pageSize: number;
  pageNumber: number;
}
