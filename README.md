### Идея
Сделать сервис для инвесторов, где они могут видеть все свои портфели в одном месте и получать по ним автоматическую аналитику.

### Проблемы
- Ручная аналитика. Трата времени на ручной расчет метрик портфеля, построение графиков и т.д.
- Проблема учета инвестиций. Несколько инвестиционных портфелей у разных брокеров.

### Основные ~~конкуренты~~ аналоги
- Intelinvest
- Investfolio
- Indexera.io
- ...

Основная их проблема — заточенность под учет портфелей, а не их аналитику.

### Аналитика портфелей
Аналитика в нашем сервисе основана на Современной портфельной теории (modern portfolio theory). Её основные положения: 
- Риск и доходность взаимосвязаны. Более высокая доходность связана с более высоким риском.
- За счет диверсификации по различным классам активов можно сгладить волатильность портфеля.

### MVP
- Парсинг отчета брокера (СберБанк)
- Восстановление портфеля
- Агрегация нескольких портфелей
- Ведение статистики
- Расчет коэффициентов из MPT
##### Коэффициенты для MVP:
- Доходность портфеля.
- Риск портфеля.
- Коэффициент Шарпа (соотношение риска к доходности).
- Коэффициент диверсификации.

##### Тестовый пользователь
Эл. почта: test@test.ru
Пароль: 123456
