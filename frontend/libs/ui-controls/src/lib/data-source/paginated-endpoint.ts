import { Observable } from 'rxjs';
import { Page } from './page';
import { PageRequest } from './page-request';

export type PaginatedEndpoint<T> = (req: PageRequest<T>) => Observable<Page<T>>;
