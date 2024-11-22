using System;

class SecantMethod
{
    // Функция для вычисления f(x) = x^2 - cos(x)
    static double Function(double x)
    {
        return x * x - Math.Cos(x);
    }

    // Метод секущих
    static (double, int) FindRootUsingSecantMethod(double x0, double x1, double epsilon, int maxIter)
    {
        if (epsilon <= 0)
            throw new ArgumentException("Tolerance (epsilon) must be positive.");

        if (maxIter <= 0)
            throw new ArgumentException("Maximum iterations must be positive.");

        double f_x0 = Function(x0);
        double f_x1 = Function(x1);
        int iterCount = 0;

        while (Math.Abs(x1 - x0) >= epsilon)
        {
            // Проверка на нулевое различие
            if (Math.Abs(f_x1 - f_x0) < 1e-12)
                throw new InvalidOperationException("Function values are too close to each other, cannot proceed.");

            // Расчет нового приближения
            double x2 = x1 - f_x1 * (x1 - x0) / (f_x1 - f_x0);

            // Обновление значений
            x0 = x1;
            x1 = x2;
            f_x0 = f_x1;
            f_x1 = Function(x1);
            iterCount++;

            // Проверка на достижение максимального количества итераций
            if (iterCount >= maxIter)
                throw new InvalidOperationException("Maximum iterations reached without convergence.");
        }

        return (x1, iterCount);
    }

    static void Main()
    {
        // Установка кодировки консоли на UTF-8
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Начальные приближения
        double x0 = -1.0;
        double x1 = -0.5;

        // Точность и максимальное количество итераций
        double epsilon = 0.01;
        int maxIterations = 100;

        try
        {
            var (root, iterations) = FindRootUsingSecantMethod(x0, x1, epsilon, maxIterations);
            Console.WriteLine($"Приближённый корень: {root}, найден за {iterations} итераций");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка ввода: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Ошибка вычислений: {ex.Message}");
        }
    }
}
