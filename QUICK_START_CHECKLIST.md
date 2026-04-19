# Box Delivery - Контрольний Список для Швидкого Запуску

## ✅ Завершено - Все скрипти готові!

Всі необхідні C# скрипти вже створені та готові до використання:

- ✅ **GameManager.cs** - Управління грою
- ✅ **PlayerMovement.cs** - Рух персонажа
- ✅ **PlayerInteraction.cs** - Взаємодія з коробками
- ✅ **BoxObject.cs** - Логіка коробок
- ✅ **DeliveryZone.cs** - Зони доставки
- ✅ **LevelManager.cs** - Спавн коробок
- ✅ **CameraController.cs** - Камера
- ✅ **DebugHelper.cs** - Допомога при тестуванні

---

## 🎯 Наступні кроки - Налаштування сцени в Unity

Дотримуйтесь цієї послідовності для швидкого запуску:

### **Крок 1: Базова підготовка** (5 хвилин)

- [ ] Відкрийте Assets/Scenes/GameScene.unity у Unity
- [ ] Збережіть сцену (Ctrl+S)
- [ ] Переконайтесь, що Project режим відкритий

### **Крок 2: Створення спрайтів** (5 хвилин)

В Hierarchy:
- [ ] Create > 2D Object > Sprites > Square (PlayerSprite) - Синій колір
- [ ] Create > 2D Object > Sprites > Square (BoxSprite) - Коричневий колір
- [ ] Create > 2D Object > Sprites > Square (DeliveryZoneSprite) - Зелений напівпрозорий
- [ ] Create > 2D Object > Sprites > Square (WallSprite) - Сірий колір

### **Крок 3: Створення персонажа** (5 хвилин)

- [ ] Create Empty > перейменуйте на "Player"
- [ ] Add Component > Sprite Renderer (призначте PlayerSprite)
- [ ] Add Component > Rigidbody 2D (Body Type: Dynamic, Gravity: 0)
- [ ] Add Component > Circle Collider 2D (Radius: 0.3)
- [ ] Add Component > PlayerMovement
- [ ] Add Component > PlayerInteraction

Під Player додайте дочірній об'єкт:
- [ ] Create Empty > CarryPoint
- [ ] Встановіть Position: (0, 0.5, 0)
- [ ] У PlayerInteraction тягніть CarryPoint у "Carry Point"

### **Крок 4: Створення коробки (Prefab)** (5 хвилин)

- [ ] Create Empty > перейменуйте на "Box"
- [ ] Add Component > Sprite Renderer (призначте BoxSprite)
- [ ] Add Component > Rigidbody 2D (Body Type: Dynamic, Gravity: 0)
- [ ] Add Component > Box Collider 2D (Size: 0.8 x 0.8)
- [ ] Add Component > BoxObject
- [ ] Тягніть "Box" у папку Assets/Prefabs
- [ ] Delete "Box" зі сцени

### **Крок 5: Створення зон доставки** (10 хвилин)

Для кожного кольору (Red, Green, Blue, Yellow, Orange):
- [ ] Create Empty > перейменуйте на "DeliveryZone_Red"
- [ ] Add Component > Sprite Renderer (призначте DeliveryZoneSprite)
- [ ] Add Component > Box Collider 2D (Is Trigger: ✅ ON)
- [ ] Add Component > DeliveryZone
- [ ] Встановіть "Zone Color" = "Red"
- [ ] Розташуйте в різних місцях сцени
- [ ] Встановіть потрібний колір спрайту

Повторіть для інших кольорів (Green, Blue, Yellow, Orange)

### **Крок 6: Створення перешкод** (5 хвилин)

- [ ] Create Empty > перейменуйте на "Walls"
- [ ] Додайте кількох дочірніх об'єктів (Wall_1, Wall_2 і т.д.)
- [ ] Для кожного:
  - [ ] Add Component > Sprite Renderer
  - [ ] Add Component > Box Collider 2D
  - [ ] Розташуйте як стіни на карті

### **Крок 7: Налаштування UI** (5 хвилин)

- [ ] Create > UI > Canvas
- [ ] Перейменуйте на "GameUI"

Додайте Text - TextMeshPro для:
- [ ] "ScoreText" в верхньому лівому куті
- [ ] "TimerText" в верхньому центрі
- [ ] "DeliveredText" в верхньому правому куті
- [ ] "MessageText" в центрі (для Game Over)
- [ ] "PauseText" в центрі (для повідомлення паузи)

### **Крок 8: Налаштування GameManager** (5 хвилин)

- [ ] Create Empty > перейменуйте на "GameManager"
- [ ] Add Component > GameManager
- [ ] В Inspector:
  - [ ] Drag ScoreText в "Score Text"
  - [ ] Drag TimerText в "Timer Text"
  - [ ] Drag DeliveredText в "Delivered Text"
  - [ ] Drag MessageText в "Message Text"
  - [ ] Drag PauseText в "Pause Text"
  - [ ] Level Time: 120
  - [ ] Boxes To Win: 3

### **Крок 9: Налаштування LevelManager** (5 хвилин)

- [ ] Create Empty > перейменуйте на "LevelManager"
- [ ] Add Component > LevelManager
- [ ] Створіть SpawnPoint_1, SpawnPoint_2, SpawnPoint_3 (пусті об'єкти)
- [ ] У LevelManager:
  - [ ] Drag Box prefab в "Box Prefab"
  - [ ] Drag усі SpawnPoints у "Spawn Points" массив
  - [ ] Spawn Interval: 2
  - [ ] Max Boxes At Once: 5

### **Крок 10: Налаштування Камери** (3 хвилини)

- [ ] Виберіть Main Camera
- [ ] Встановіть:
  - [ ] Position: (0, 0, -10)
  - [ ] Size: 8
- [ ] Add Component > CameraController

### **Крок 11: Фінальна перевірка** (3 хвилини)

- [ ] Нажміть Play ▶️
- [ ] Перевірте:
  - [ ] Персонаж рухається (WASD/стрілки)
  - [ ] Появляються коробки
  - [ ] Можна підняти коробку (E)
  - [ ] Таймер відраховується
  - [ ] Паузу можна включити (ESC)
  - [ ] При доставці в зону звільняється коробка

---

## 🚀 Тестові клавіші (DebugHelper)

Додатково в скрипті DebugHelper є тестові клавіші:
- **T** - Додати 10 очок (тест)
- **R** - Перезавантажити гру
- **D** - Вивести статус у консоль
- **B** - Сигнал про доставку коробки (тест)

---

## 📋 Залежності та обов'язкові компоненти

**Player ПОВИНЕН мати**:
- ✅ PlayerMovement
- ✅ PlayerInteraction
- ✅ Rigidbody 2D
- ✅ Circle Collider 2D
- ✅ Sprite Renderer

**Box Prefab ПОВИНЕН мати**:
- ✅ BoxObject
- ✅ Rigidbody 2D
- ✅ Box Collider 2D
- ✅ Sprite Renderer

**DeliveryZone ПОВИНЕН мати**:
- ✅ DeliveryZone
- ✅ Box Collider 2D (Is Trigger: ON!)
- ✅ Sprite Renderer

**Сцена ПОВИННА мати**:
- ✅ GameManager (синглтон)
- ✅ LevelManager
- ✅ Canvas з UI текстом
- ✅ Main Camera

---

## ⚠️ Часті помилки та рішення

| Проблема | Рішення |
|----------|---------|
| Коробки не спавняються | Перевірте Box prefab у LevelManager |
| Персонаж не рухається | Додайте Rigidbody 2D та PlayerMovement |
| Коробки не піднімаються | Перевірте PlayerInteraction та CarryPoint |
| Коробки не зникають | Встановіть Is Trigger = ON у DeliveryZone |
| Таймер не працює | Переконайтесь, що GameManager у сцені |
| Паузу не можна включити | Перевірте ESC клавішу у GameManager |

---

## 📱 Розміри об'єктів (рекомендовані)

- **Player**: 0.5 x 0.5 (или як персонаж виглядає добре)
- **Box**: 0.8 x 0.8
- **DeliveryZone**: 1.5 x 1.5
- **Wall**: 2 x 10 (або як потрібно)

---

## 🎨 Кольори (Hex коди)

- **Red**: #FF0000
- **Green**: #00FF00
- **Blue**: #0000FF
- **Yellow**: #FFFF00
- **Orange**: #FF8000
- **Gray** (стіни): #808080

---

## ⏱️ Орієнтовна тривалість

- Крок 1: 5 хвилин
- Крок 2: 5 хвилин
- Крок 3-4: 10 хвилин
- Крок 5: 10 хвилин
- Крок 6-7: 10 хвилин
- Крок 8-9: 10 хвилин
- Крок 10-11: 6 хвилин

**Разом: ~60 хвилин**

---

## 📚 Допоміжні ресурси

Створені документи у проєкті:
- `README.md` - Повна документація
- `SETUP_GUIDE.md` - Детальна інструкція налаштування
- `SCRIPTS_REFERENCE.md` - Довідник по всіх скриптам

---

## ✨ Успіхів!

Якщо дотримуватися цього плану, гра повинна запуститися за ~60 хвилин.

Питання? Перевірте SCRIPTS_REFERENCE.md або консоль Unity!

Гарної розробки! 🎮
