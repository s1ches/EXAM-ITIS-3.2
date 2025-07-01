# Экзамен по .NET 3 курс 2 семестр

### Задание: 

graphql + kafka. У вас должен крутиться сервис кафки с zookeeper в докере. Логика такая: через graphql отправляем текст, текст идет в кафку, подписчик ловит сообщение из кафки, с делеем обрабатывает его и кладет в коллекцию в памяти. С помощью постмана (или banancakepop) отправлять запросы на:
добавление элемента
получение cписка сообщений из памяти

### Схема работы приложения:

![telegram-cloud-photo-size-2-5411571330702242723-y](https://github.com/user-attachments/assets/ddca5332-0a4c-4c38-a942-290bd2c8c2bc)

### Как запустить

RUN DOCKER COMPOSE, RUN! 

Затем в браузере: `http://localhost:5112/graphql`

### Запроосы для тестирования:

Добавить запись:

```graphql
mutation {
  addText(text: "t0")
}
```

Получить записи:

```graphql
query {
  texts {
    totalCount
    items {
      content
      createdAt
      id
    }
  }
```





