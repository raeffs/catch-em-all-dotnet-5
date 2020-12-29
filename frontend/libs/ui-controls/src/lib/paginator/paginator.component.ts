import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { take } from 'rxjs/operators';
import { PaginatedDataSource } from '../data-source';

@Component({
  selector: 'cea-paginator',
  templateUrl: 'paginator.component.html',
  styleUrls: ['paginator.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PaginatorComponent {
  @Input()
  public dataSource: PaginatedDataSource<unknown> | null = null;

  public nextPage(): void {
    this.dataSource?.page$.pipe(take(1)).subscribe(p => {
      this.dataSource?.fetch(p.pageNumber + 1);
    });
  }

  public prevPage(): void {
    this.dataSource?.page$.pipe(take(1)).subscribe(p => {
      this.dataSource?.fetch(p.pageNumber - 1);
    });
  }
}
