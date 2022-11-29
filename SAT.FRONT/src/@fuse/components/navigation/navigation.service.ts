import { Injectable } from '@angular/core';
import { FuseNavigationItem } from '@fuse/components/navigation/navigation.types';

@Injectable({
    providedIn: 'root'
})
export class FuseNavigationService
{
    private _componentRegistry: Map<string, any> = new Map<string, any>();
    private _navigationStore: Map<string, FuseNavigationItem[]> = new Map<string, any>();

    registerComponent(name: string, component: any): void
    {
        this._componentRegistry.set(name, component);
    }

    deregisterComponent(name: string): void
    {
        this._componentRegistry.delete(name);
    }

    getComponent<T>(name: string): T
    {
        return this._componentRegistry.get(name);
    }

    storeNavigation(key: string, navigation: FuseNavigationItem[]): void
    {
        this._navigationStore.set(key, navigation);
    }

    getNavigation(key: string): FuseNavigationItem[]
    {
        return this._navigationStore.get(key) ?? [];
    }

    deleteNavigation(key: string): void
    {
        this._navigationStore.delete(key);
    }

    getFlatNavigation(navigation: FuseNavigationItem[], flatNavigation: FuseNavigationItem[] = []): FuseNavigationItem[]
    {
        for (const item of navigation)
        {
            if (item.type === 'basic')
            {
                flatNavigation.push(item);
                continue;
            }

            if (item.type === 'aside' || item.type === 'collapsable' || item.type === 'group')
            {
                if (item.children)
                {
                    this.getFlatNavigation(item.children, flatNavigation);
                }
            }
        }

        return flatNavigation;
    }

    getItem(id: string, navigation: FuseNavigationItem[]): FuseNavigationItem | null
    {
        for (const item of navigation)
        {
            if (item.id === id)
            {
                return item;
            }

            if (item.children)
            {
                const childItem = this.getItem(id, item.children);

                if (childItem)
                {
                    return childItem;
                }
            }
        }

        return null;
    }

    getItemParent(
        id: string,
        navigation: FuseNavigationItem[],
        parent: FuseNavigationItem[] | FuseNavigationItem
   ): FuseNavigationItem[] | FuseNavigationItem | null
    {
        for (const item of navigation)
        {
            if (item.id === id)
            {
                return parent;
            }

            if (item.children)
            {
                const childItem = this.getItemParent(id, item.children, item);

                if (childItem)
                {
                    return childItem;
                }
            }
        }

        return null;
    }
}
