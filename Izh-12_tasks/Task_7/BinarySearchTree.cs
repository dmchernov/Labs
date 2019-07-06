using System.Collections.Generic;

namespace Izh_12_tasks.Task_7
{
    /// <summary>
    /// Бинарное дерево поиска. Содержит только уникальные значения.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinarySearchTree<T>
    {
        /// <summary>
        /// Вложенный класс узла дерева.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Node<T>
        {
            public Node<T> left;
            public Node<T> right;
            public Node<T> parent;
            public T key;

            public Node(Node<T> left, T key, Node<T> parent, Node<T> right)
            {
                this.key = key;
                this.left = left;
                this.right = right;
                this.parent = parent;
            }
        }

        public Node<T> rootNode;
        private IComparer<T> objectComparer;

        public BinarySearchTree()
        {
            objectComparer = Comparer<T>.Default;
        }

        public BinarySearchTree(IComparer<T> comparer)
        {
            objectComparer = comparer;
        }

        /// <summary>
        /// Создаёт и наполняет дерево указанными значениями с указанием собственного порядка сравнения.
        /// </summary>
        /// <param name="comparer"></param>
        /// <param name="values"></param>
        public BinarySearchTree(IComparer<T> comparer, params T[] values) : this(comparer)
        {
            foreach (var item in values)
            {
                Insert(item);
            }
        }

        /// <summary>
        /// Создаёт и наполняется дерево указанными значениями.
        /// </summary>
        /// <param name="values">Значения, которыми наполняется дерево.</param>
        public BinarySearchTree(params T[] values) : this()
        {
            foreach (var item in values)
            {
                Insert(item);
            }
        }

        /// <summary>
        /// Поиск узла дерева по указанному ключу.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <returns>Узел дерева.</returns>
        public Node<T> Find(T key)
        {
            return Find(rootNode, key);
        }

        /// <summary>
        /// Добавление ключа в дерево.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <returns>True -- элемент добавлен успешно, False -- такой элемент уже существует в дереве.</returns>
        public bool Insert(T key)
        {
            return Insert(ref rootNode, key, null);
        }

        /// <summary>
        /// Удаление узла с указанным ключом из дерева.
        /// </summary>
        /// <param name="key">Ключ узла.</param>
        /// <returns>True -- в случае успеха, иначе False.</returns>
        public bool Remove(T key)
        {
            return Remove(rootNode, key);
        }

        /// <summary>
        /// Обход дерева по принципу: корень, левое поддерево, правое поддерево.
        /// </summary>
        /// <param name="node">Узел дерева.</param>
        /// <returns></returns>
        public IEnumerable<T> Preorder(Node<T> node)
        {
            if (node == null)
            {
                yield break;
            }

            yield return node.key;

            var iterator = Preorder(node.left).GetEnumerator();
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }

            iterator = Preorder(node.right).GetEnumerator();
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }
        }

        /// <summary>
        /// Обход дерева по принципу: левое поддерево, корень, правое поддерево.
        /// </summary>
        /// <param name="node">Узел дерева.</param>
        /// <returns></returns>
        public IEnumerable<T> Inorder(Node<T> node)
        {
            if (node == null)
            {
                yield break;
            }

            var iterator = Inorder(node.left).GetEnumerator();
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }

            yield return node.key;

            iterator = Inorder(node.right).GetEnumerator();
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }
        }

        /// <summary>
        /// Обход дерева по принципу: левое поддерево, правое поддерево, корень.
        /// </summary>
        /// <param name="node">Узел дерева.</param>
        /// <returns></returns>
        public IEnumerable<T> Postorder(Node<T> node)
        {
            if (node == null)
            {
                yield break;
            }

            var iterator = Postorder(node.left).GetEnumerator();
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }

            iterator = Postorder(node.right).GetEnumerator();
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }

            yield return node.key;
        }

        private Node<T> Find(Node<T> current, T key)
        {
            if (objectComparer.Compare(key, current.key) == 0)
            {
                return current;
            }

            if (objectComparer.Compare(key, current.key) < 0)
            {
                return Find(current.left, key);
            }
            else
            {
                return Find(current.right, key);
            }
        }

        private bool Insert(ref Node<T> current, T key, Node<T> parent)
        {
            if (current == null)
            {
                current = new Node<T>(null, key, parent, null);
                if (parent == null)
                {
                    rootNode = current;
                }

                return true;
            }

            if (objectComparer.Compare(key, current.key) > 0)
            {
                return Insert(ref current.right, key, current);
            }
            else if (objectComparer.Compare(key, current.key) < 0)
            {
                return Insert(ref current.left, key, current);
            }
            else
            {
                return false;
            }
        }


        private Node<T> FindMinNodeInSubtree(Node<T> tree)
        {
            while (tree.left != null)
            {
                tree = tree.left;
            }

            return tree;
        }

        private void ReplaceNode(Node<T> root, Node<T> successor)
        {
            root.key = successor.key;
            root.left = successor.left;
            root.right = successor.right;
            successor = null;
        }

        private bool Remove(Node<T> root, T key)
        {
            if (objectComparer.Compare(key, root.key) < 0)
            {
                return Remove(root.left, key);
            }

            if (objectComparer.Compare(key, root.key) > 0)
            {
                return Remove(root.right, key);
            }

            if ((root.left != null) && (root.right != null))
            {
                Node<T> successor = FindMinNodeInSubtree(root.right);
                root.key = successor.key;
                return Remove(successor, successor.key);
            }
            else if (root.left != null)
            {
                ReplaceNode(root, root.left);
            }
            else if (root.right != null)
            {
                ReplaceNode(root, root.right);
            }
            else
            {
                if (root == root.parent.left)
                {
                    root.parent.left = null;
                }

                if (root == root.parent.right)
                {
                    root.parent.right = null;
                }

                root = null;
            }

            return true;
        }
    }
}
