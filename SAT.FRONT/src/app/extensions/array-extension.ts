// export { };

// export class GroupBy {
//     public key: string;
//     public value: any[] = [];
// }

// Array.prototype.groupBy = function (this: Array<any>, key: string): GroupBy[] {
//     let newGroupByList: GroupBy[] = [];
//     this.forEach(element => {
//         if (newGroupByList.some(s => { return s.key == element[key] })) {
//             newGroupByList.find(m => m.key == element[key]).value.push(element);
//         }
//         else {
//             let group = new GroupBy();
//             group.key = element[key];
//             group.value.push(element);
//             newGroupByList.push(group);
//         }
//     });
//     return newGroupByList;
// };

// const order = function (list: Array<any>, property: string, order: string): any[] {
//     let direction = order == 'asc' ? -1 : 1;
//     list.sort(function (a, b) {
//         if (a[property] < b[property]) {
//             return -1 * direction;
//         } else if (a[property] > b[property]) {
//             return 1 * direction;
//         } else {
//             return 0;
//         }
//     });
//     return list;
// };

// Array.prototype.orderBy = function (this: Array<any>, property: string): any[] {
//     return order(this, property, 'asc');
// };

// Array.prototype.thenBy = function (this: Array<any>, property: string): any[] {
//     return order(this, property, 'asc');
// };

// Array.prototype.orderByDesc = function (this: Array<any>, property: string): any[] {
//     return order(this, property, 'desc');
// };

// Array.prototype.thenByDesc = function (this: Array<any>, property: string): any[] {
//     return order(this, property, 'desc');
// };

// Array.prototype.sum = function (this: Array<any>, property: string): number {
//     return this.reduce((ty, u) => ty + u[property], 0);
// };

// Array.prototype.take = function (this: Array<any>, take: number): any[] {
//     return this.slice(0, take);
// };