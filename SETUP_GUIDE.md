# Box Delivery - Швидка інструкція по настройці

## Шаг 1: Створення простих спрайтів

У Unity використовуйте вбудовані примітиви або створіть прості кольорові квадрати:

### Для персонажа:
1. Create > 2D Object > Sprites > Square
2. Перейменуйте в "PlayerSprite"
3. Змініть колір (синій) у SpriteRenderer

### Для коробок:
1. Create > 2D Object > Sprites > Square
2. Перейменуйте в "BoxSprite"
3. Змініть розмір: 0.8 x 0.8
4. Змініть колір (коричневий)

### Для зон доставки:
1. Create > 2D Object > Sprites > Square
2. Перейменуйте в "DeliveryZoneSprite"
3. Змініть розмір: 1.5 x 1.5
4. Змініть колір (зелений, напівпрозорий - Alpha: 0.3)

### Для стін/підлог:
1. Create > 2D Object > Sprites > Square
2. Змініть розмір як потрібно (2 x 10 тощо)
3. Колір: сірий

## Шаг 2: Створення об'єктів у сцені

### А) Створення гравця:
1. Create Empty GameObject, перейменуйте в "Player"
2. Додайте Components:
   - Sprite Renderer (drag PlayerSprite)
   - Rigidbody 2D (Body Type: Dynamic, Gravity Scale: 0)
   - Circle Collider 2D (Radius: 0.3)
   - PlayerMovement script
   - PlayerInteraction script

3. Створіть пустий об'єкт як дочірній "Player" з назвою "CarryPoint"
   - Position: (0, 0.5, 0)
   - Це точка, де будуть утримуватися коробки

4. У PlayerInteraction:
   - Drag CarryPoint у поле "Carry Point"

### Б) Створення префаба коробки:
1. Create Empty GameObject, перейменуйте в "Box"
2. Додайте Components:
   - Sprite Renderer (drag BoxSprite)
   - Rigidbody 2D (Body Type: Dynamic, Gravity Scale: 0)
   - Box Collider 2D (size: 0.8 x 0.8)
   - BoxObject script

3. Перетягніть Box у папку Prefabs
4. Видаліть Box зі сцени

### В) Створення зон доставки:
Для кожного кольору (Red, Green, Blue, Yellow, Orange):

1. Create Empty GameObject, перейменуйте в "DeliveryZone_Red"
2. Додайте Components:
   - Sprite Renderer (drag DeliveryZoneSprite)
   - Box Collider 2D (Is Trigger: ON, size: 1.5 x 1.5)
   - DeliveryZone script

3. У DeliveryZone script: встановіть "Zone Color" = "Red"
4. Розташуйте в різних місцях сцени
5. Встановіть різні кольори спрайтів для кожної зони

### Г) Створення стін/перешкод:
1. Create Empty GameObject, перейменуйте в "Walls"
2. Створіть кілька дочірніх об'єктів Wall_1, Wall_2 тощо
3. Для кожного:
   - Add Sprite Renderer (квадрат)
   - Add Box Collider 2D (розмір під спрайт)

## Шаг 3: Настройка Canvas та UI

1. Create > UI > Canvas
2. Перейменуйте в "GameUI"

### Створіть текстові елементи (Text - TextMeshPro):
   - "ScoreText" - Score: 0
   - "TimerText" - Time: 60
   - "DeliveredText" - Delivered: 0/3
   - "MessageText" - (для Game Over/You Win)
   - "PauseText" - (для повідомлення паузи)

Розташуйте їх на екрані у зручних позиціях.

## Шаг 4: Настройка GameManager

1. Створіть пустий об'єкт "GameManager"
2. Add GameManager script
3. У Inspector:
   - Drag ScoreText у "Score Text"
   - Drag TimerText у "Timer Text"
   - Drag DeliveredText у "Delivered Text"
   - Drag MessageText у "Message Text"
   - Drag PauseText у "Pause Text"
   - Встановіть Level Time: 120 (120 секунд)
   - Встановіть Boxes To Win: 3

## Шаг 5: Настройка LevelManager

1. Створіть пустий об'єкт "LevelManager"
2. Add LevelManager script
3. Створіть кілька пустих об'єктів як "SpawnPoint_1", "SpawnPoint_2" тощо
4. У LevelManager:
   - Drag Box префаб у "Box Prefab"
   - Drag усі SpawnPoints у масив "Spawn Points"
   - Встановіть Spawn Interval: 2 (спавн кожні 2 секунди)
   - Встановіть Max Boxes At Once: 5

## Шаг 6: Настройка Камери

1. Виберіть main camera
2. Встановіть Position: (0, 0, -10)
3. Встановіть Size: 8 (для хорошої видимості)
4. Add CameraController script (опціонально для слідування за гравцем)

## Шаг 7: Тестування

1. Нажміть Play
2. Використовуйте WASD або стрілки для руху
3. Нажміть E для підняття/опускання коробок
4. Нажміть ESC для паузи
5. Доставляйте коробки у відповідні кольорові зони

## Додатково:

Якщо ви хочете поліпшити гру:
- Додайте звукові ефекти
- Створіть спрайти кращої якості (використовуйте Kenney assets)
- Додайте анімації руху персонажа
- Реалізуйте різні рівні складності
- Додайте рухомі перешкоди
- Створіть головне меню
- Реалізуйте систему очок

## Потенційні проблеми та рішення

### Коробки не спавняються:
- Перевірте, чи призначений Box префаб у LevelManager
- Переконайтесь, що spawn points створені та назначені

### Персонаж не рухається:
- Перевірте, чи Player має Rigidbody 2D компонент
- Переконайтесь, що PlayerMovement script додан

### Коробки не піднімаються:
- Перевірте, чи Player має PlayerInteraction компонент  
- Переконайтесь, що carryPoint назначена

### Коробки не зникають при доставці:
- Перевірте, чи DeliveryZone має is Trigger = ON
- Переконайтесь, що зоні назначен правильний колір
- Переконайтесь, що GameManager присутній у сцені

### Таймер не відстає:
- Перевірте, чи GameManager присутній у сцені
- Переконайтесь, що TimerText назначена
- Переглядайте консоль на помилки

