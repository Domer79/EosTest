# Что было сделано и как работает

1. Механизм инициализации базы при запуске приложения, таким образом, чтобы при запуске приложения бэкенда создавалась пустая база данных. Строка подключения такая: `Data Source=.;Initial Catalog=EosTest;Integrated Security=True`, локально должно подключиться.
2. В коде зашит словарь слов, около 3000, служит для инициализации тестовых данных. Данные добавляются в виде небинарного дерева, количество дочерних узлов первого уровня произвольного узла формируется случайным образом ои 1 до 5, глубина узлов не более 20. Для индексации всех дочерних узлов произвольного узла в глубину служит таблица GlobalItems.
3. Запустить UI можно командой `npm run dev:local`. Дев-сервер настроен на порт 3100 (http://localhost:3100).
4. После запуска UI нужно сгенерировать данные, для этого служит пункт меню "Обновление данных", процесс сначала производит полное удаление данных, а уже после создает заново. Процесс длительный, иожет занять какое-то время.
5. На странице узловые данные, верхняя таблица представляет собой список узлов, у которых есть дочерние элементы. При выборе строки на чекбоксе, ниже появляется таблица со списком дочерних элементов, выбранного узла. Я не совсем понял про вывод максимального значения в списке, поэтому сделал, чтобы оно приходило отдельным полем по тому же запросу, что и список. Вверху таблицы с дочерними узлами отображается значение максимального отклонения. 
6. Кнопки перехода по страницам таблиц кликабельны.
7. Также для проверки правильности алгоритма поиска дочерних узлов и скорости выполнения запроса, сделан второй вариант запроса с помощью обобщенного табличного выражения (ОТВ), доступно из пункта меню "Узловые элементы (ОТВ)". При этом страница не перезагружается, меняется строка URL (решение не совсем верное) и перезагружаются данные в таблицах.
8. Сравнение планов выполнения обоих запросов и их проверка опытным путем показали, что индексация идентификаторов в отдельной таблице работает эффективнее.