// Le Sujet est l'objet qui est observé (player)
public interface ISubject
{
    void Attach(IObserver observer);

    void Detach(IObserver observer);

    void Notify();
}