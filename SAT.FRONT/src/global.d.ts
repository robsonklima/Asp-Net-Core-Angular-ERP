import { GroupBy } from "./app/extensions/array-extension";

export { };
declare global {
  interface Array<T> {
    groupBy(this: Array<T>, key: string): GroupBy[];
  }
}